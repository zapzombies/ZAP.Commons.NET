using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZAP.Commons.Extensions;

namespace ZAP.Commons.Tests
{
    public class DictionaryExtensionTests
    {
        public static List<object[]> DifferentKeys = new()
        {
            new object[] {
                new List<KeyValuePair<string, int>>()
                {
                    new KeyValuePair<string, int>("hello", 1),
                    new KeyValuePair<string, int>("hi", 2),
                    new KeyValuePair<string, int>("t", 3)
                }
            },
            new object[] {
                new List<KeyValuePair<string, int>>()
                {
                    new KeyValuePair<string, int>("xd", 0),
                    new KeyValuePair<string, int>("hi", 0),
                    new KeyValuePair<string, int>("t", 0)
                }
            },
        };

        public static List<object[]> SameKeys = new()
        {
            new object[] {
                "test 1",
                new List<object>() { "hi", "omg", 1, null, "expected" }
            },
        };

        public static List<object[]> TypedDictionary = new()
        {
            new object[]
            {
                // Same datatype test
                new Dictionary<Type, object>()
                {
                    [typeof(string)] = "whatever",
                    [typeof(int)] = "bro",
                },
                typeof(int),
                "bro"
            }
        };

        [Theory]
        [MemberData(nameof(DifferentKeys))]
        public void AddOrModify_AddDifferentKeys_ShouldAdd(List<KeyValuePair<string, int>> inputs)
        {
            var dict = new Dictionary<string, int>();
            foreach (var item in inputs)
            {
                dict.AddOrModify(item.Key, item.Value);
            }

            Assert.Equal(inputs.Count, dict.Count);
        }


        [Theory]
        [MemberData(nameof(SameKeys))]
        public void AddOrModify_AddSameKeys_ShouldModify(string keyName, List<object> values)
        {
            var dict = new Dictionary<string, object>();
            foreach (var item in values)
            {
                dict.AddOrModify(keyName, item);
            }

            var keyPair = Assert.Single(dict);
            Assert.Equal(values.Last(), keyPair.Value);
        }

        [Theory]
        [MemberData(nameof(TypedDictionary))]
        public void GetSelfOrNearestAncestor_FindSelfOrAncestor_ShouldReturns(Dictionary<Type, object> testSamples, Type type, object expectedValue)
        {
            var actual = testSamples.GetSelfOrNearestAncestor(type);

            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public void Concat_Concat2FilledDictionary_ShouldMergeToBaseDict()
        {
            var baseDict = new Dictionary<string, object>()
            {
                ["member1"] = "test",
                ["member2"] = DateTime.Now
            };

            var targetDict = new Dictionary<string, object>()
            {
                ["member3"] = 2021,
                ["member4"] = null
            };

            baseDict.Concat(targetDict);

            Assert.Equal(4, baseDict.Count);
        }
    }
}
