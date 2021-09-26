﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace theRightDirection.Tests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void EnterpriseUrl_Is_AlphaNumeric_And_Special_Character()
        {
            char[] chars2 = { ':' };
            var url = "trdgis.svwxb2emmfbudlqtj25tgxyjla.frax.internal.cloudapp.net:7443";
            url.IsAlphaNumeric(chars2).Should().BeTrue();
        }
        [TestMethod]
        public void Null_To_UnSecureString()
        {
            var x = new SecureStringProperty();
            x.Name2.Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void Null_To_SecureString()
        {
            var x = new SecureStringProperty();
            x.Name = null;
            x.Name.Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void SplitAtCapitals_Works_Good()
        {
            var text = "MannusAndVerena";
            var result = text.SplitOnCapitalLetters();
            result.Should().Be("Mannus And Verena");
        }
        [TestMethod]
        public void SplitAtCapitals_Works_Not_On_A_String_With_No_Capitals()
        {
            var text = "mannusandverena";
            var result = text.SplitOnCapitalLetters();
            result.Should().Be("mannusandverena");
        }
        [TestMethod]
        public void SplitAtCapitals_Works_Not_On_A_String_With_Only_Capital_At_The_Beginning()
        {
            var text = "Mannusandverena";
            var result = text.SplitOnCapitalLetters();
            result.Should().Be("Mannusandverena");
        }
        [TestMethod]
        public void SplitAtCapitals_Works_Not_On_A_Empty_String()
        {
            var text = "";
            var result = text.SplitOnCapitalLetters();
            result.Should().Be("");
        }
        [TestMethod]
        public void SplitAtCapitals_Works_Not_On_A_Null_String()
        {
            string text = null;
            var result = text.SplitOnCapitalLetters();
            result.Should().Be("");
        }

        [TestMethod]
        public void IsValidJson_Is_False1()
        {
            string json = "{abc";
            var result = json.IsValidJson();
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidJson_Is_False2()
        {
            string json = "[{abc]";
            var result = json.IsValidJson();
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidJson_Is_True2()
        {
            string json = "{\"component\":1,\"releaseDate\":\"2020-12-21T00:00:00+01:00\",\"versionNumber\":{\"major\":2021,\"minor\":2,\"build\":0,\"revision\":-1,\"majorRevision\":-1,\"minorRevision\":-1},\"preRelease\":false}";
            var result = json.IsValidJson();
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidJson_Is_True()
        {
            string json = "{\"component\":1,\"releaseDate\":\"2020-12-21T00:00:00+01:00\",\"versionNumber\":{\"major\":2021,\"minor\":2,\"build\":0,\"revision\":-1,\"majorRevision\":-1,\"minorRevision\":-1},\"preRelease\":false}";
            var result = json.IsValidJson<JsonClass>();
            result.Should().BeTrue();
        }
    }

    internal class JsonClass
    {
        public JsonClassComponent Component { get; set; }
        public DateTime ReleaseDate { get; set; }

        [JsonIgnore]
        public Version VersionNumber { get; set; }

        public bool PreRelease { get; set; }
    }

    public enum JsonClassComponent
    {
        /// <summary>
        /// Foutieve waarde
        /// </summary>
        Unknown,

        /// <summary>
        /// WPF-desktop applicatie
        /// </summary>
        Desktop
    }

    public class SecureStringProperty
    {
        private SecureString _name, _name2;
        public string Name
        {
            get => _name.ToUnsecureString();
            set => _name = value.ToSecureString(false,true);
        }
        public string Name2
        {
            get => _name2.ToUnsecureString(false,true);
        }
    }
}