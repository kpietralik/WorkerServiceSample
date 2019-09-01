﻿using System;
using System.Threading.Tasks;
using WorkerServiceSample;

namespace WorkerServiceSampleUnitTests
{
    public class IpfyClientMock : IIpfyClient
    {
        public Task<string> GetIpAddress()
        {
            throw new ArgumentOutOfRangeException("TEST"); // TEMP, for tests
        }
    }
}
