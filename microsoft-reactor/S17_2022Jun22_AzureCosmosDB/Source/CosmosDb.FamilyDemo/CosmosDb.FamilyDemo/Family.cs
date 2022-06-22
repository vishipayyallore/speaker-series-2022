using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDb.FamilyDemo
{

    public class Family
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        
        public string LastName { get; set; }
        
        public Parent[] Parents { get; set; }
        
        public Child[] Children { get; set; }
        
        public Address Address { get; set; }
        
        public bool IsRegistered { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
