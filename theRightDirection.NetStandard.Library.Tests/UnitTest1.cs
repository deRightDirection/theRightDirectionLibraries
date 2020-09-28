using System;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.Library;

namespace theRightDirection.NetStandard.Library.Tests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void Webclient_Timeout_Download_Timed_Out()
        {
            var x = new TimeoutWebClient(1);
            x.Invoking(y => y.DownloadString("http://www.therightdirection.nl")).Should().Throw<WebException>();
        }
        [TestMethod]
        public void Webclient_Timeout_Download_Sucessfull()
        {
            var x = new TimeoutWebClient();
            x.Invoking(y => y.DownloadString("http://www.therightdirection.nl")).Should().NotThrow<WebException>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var x = "Mannus";
            var encryptedX = x.Encrypt("Etten");
            Assert.AreEqual("0txln/1QimcoEH9SELjXLg==", encryptedX);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var x = "0txln/1QimcoEH9SELjXLg==";
            var decryptedX = x.Decrypt("Etten");
            Assert.AreEqual("Mannus", decryptedX);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var x = "<P> (Kantoor)meubilair(tafels,</P>";
            var y = x.RemoveInvisibleCharacters();
            Assert.AreNotEqual(x.Length, y.Length);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var x = string.Empty;
            var y = x.RemoveInvisibleCharacters();
            Assert.AreEqual(x.Length, y.Length);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var x = "Het onderhoud vindt plaats in het beheergebied van Delfland, langs de Delflandse dijk vanaf Maassluis tot Hoek van Holland en langs de Delflandse kust vanaf Hoek van Holland tot de grens met Het Hoogheemraadschap van Rijnland te Wassenaar.<br/>-	De oppervlakte van het te maaien grasgewas is ca. 5.400 are, de lengte van de uit te maaien en te schonen watergangen betreft ca. 10.000 meter;<br/>-	Voor alle werkzaamheden is tevens het verwerken van de het maaisel van toepassing;<br/>-	Onderhoud aan dienstwegen, afrastering, conserveren palen/borden/afsluitbomen/hekken, beplanting, slaperdijken, strandslagen, kerven e.d.<br/><br/><omschrijving overnemen uit beschrijvend document - paragraaf ..><br/><br/>";
            var y = x.RemoveInvisibleCharacters();
            Assert.AreNotEqual(x.Length, y.Length);

        }
    }
}
