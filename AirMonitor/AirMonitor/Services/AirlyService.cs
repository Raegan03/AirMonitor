﻿using AirMonitor.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AirMonitor.Services
{
    public static class AirlyService
    {
        public static AirlyConfig AirlyConfig { get; set; }

        public static async Task<(List<Installation> installations, List<Measurement> measurements)> GetDataFromAirly()
        {
            var airlyInstallations = await GetNearestInstallations();
            if (airlyInstallations == null)
                return (new List<Installation>(), new List<Measurement>());

            var airlyMeasurements = new List<Measurement>();

            foreach (var installation in airlyInstallations)
            {
                var airlyMesurements = await GetMeasurementsById(installation.Id);
                if (airlyMesurements != null)
                {
                    airlyMesurements.Installation = installation;
                    airlyMeasurements.Add(airlyMesurements);
                }
            }
            return (airlyInstallations, airlyMeasurements);
        }

        public static async Task<Measurement> GetMeasurementFromAirly(string installationId)
            => await GetMeasurementsById(installationId);

        private static async Task<List<Installation>> GetNearestInstallations()
        {
            var location = await GetGeolocation();
            if(location == null)
            {
                Console.WriteLine("Location error!");
                return null;
            }

            using (HttpClient client = new HttpClient())
            {
                ApplyHeaders(client);
                var queries = new[]
 {
                    $"lat={location.Latitude}",
                    $"lng={location.Longitude}",
                    $"maxDistanceKM=-1",
                    $"maxResults=10",
                };

                var uri = BuildGetUri(AirlyConfig.ApiInstallationsNearest, null, queries);
                try
                {
                    var response = await client.GetAsync(uri);
                    if(response != null && response.IsSuccessStatusCode)
                    {
                        var resTxt = await response.Content.ReadAsStringAsync();
                        foreach (var item in response.Headers.GetValues("X-RateLimit-Remaining-day"))
                            Console.WriteLine(item);

                        return JsonConvert.DeserializeObject< List<Installation>>(resTxt);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (ArgumentNullException argumentError)
                {
                    return null;
                }
                catch (HttpRequestException httpError)
                {
                    return null;
                }
            };
        }

        private static async Task<Measurement> GetMeasurementsById(string installationId)
        {
            using (HttpClient client = new HttpClient())
            {
                ApplyHeaders(client);
                var queries = new[]
 {
                    $"installationId={installationId}",
                };

                var uri = BuildGetUri(AirlyConfig.ApiMeasurementsById, null, queries);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var resTxt = await response.Content.ReadAsStringAsync();
                        foreach (var item in response.Headers.GetValues("X-RateLimit-Remaining-day"))
                            Console.WriteLine(item);

                        return JsonConvert.DeserializeObject<Measurement>(resTxt);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (ArgumentNullException argumentError)
                {
                    return null;
                }
                catch (HttpRequestException httpError)
                {
                    return null;
                }
            };
        }

        private static async Task<Xamarin.Essentials.Location> GetGeolocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }
            return null;
        }

        private static void ApplyHeaders(HttpClient client)
        {
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("apikey", AirlyConfig.ApiKey);
        }

        private static Uri BuildGetUri(string argument, string argumentValue = null, string[] queries = null)
        {
            var apiUriWithArg = AirlyConfig.ApiUri + string.Format(argument, argumentValue);
            var uriBuilder = new UriBuilder(apiUriWithArg);
            if (queries != null)
            {
                foreach (var query in queries)
                {
                    uriBuilder.Query += $"{query}&";
                }
            }
            return uriBuilder.Uri;
        }
    }
}
