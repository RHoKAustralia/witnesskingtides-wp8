using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KingTides.Core.Api.Communication;
using NUnit.Framework;

namespace KingTides.Core.Tests.Api.Communication
{
    // you will need to have node running the app locally
    [TestFixture, Category("Integration")]
    public class ClientTests
    {
        [Test]
        public async void CanGetEvents()
        {
            var client = new Client("http://localhost:3000", new WebRequestFactory());
            var events = await client.GetKingTideEventsAsync();
            Assert.NotNull(events);
        }

        [Test]
        public async void CanGetFutureEvents()
        {
            var client = new Client("http://localhost:3000", new WebRequestFactory());
            var events = await client.GetFutureKingTideEventsAsync();
            Assert.NotNull(events);
        }

        [Test]
        public async void CanGetCurrentEvents()
        {
            var client = new Client("http://localhost:3000", new WebRequestFactory());
            var events = await client.GetCurrentKingTideEventsAsync();
            Assert.NotNull(events);
        }

        [Test]
        public async void CanGetPhotos()
        {
            var client = new Client("http://localhost:3000", new WebRequestFactory());
            var events = await client.GetPhotosForEmailAsync("tarcio@gmail.com");
            Assert.NotNull(events);
        }

        [Test]
        public async void CanSearchPhotos()
        {
            var client = new Client("http://localhost:3000", new WebRequestFactory());
            var events = await client.GetPhotosForRangeAsync();
            Assert.NotNull(events);
        }
    }
}
