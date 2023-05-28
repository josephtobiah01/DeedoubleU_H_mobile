using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DrakeWorkwise.Models
{
    public class GetDataRaw
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("crisis_contact")]
        public IList<CrisisContact> CrisisContact { get; set; }

        [JsonProperty("resources")]
        public Resources Resources { get; set; }

        [JsonProperty("pages")]
        public IList<Pages> Pages { get; set; }

        [JsonProperty("settings")]
        public IList<Settings> Settings { get; set; }

        [JsonProperty("country")]
        public IList<Country> Country { get; set; }
    }

    public class CrisisContact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("contact_number")]
        public string ContactNumber { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Resources
    {
        [JsonProperty("article")]
        public IList<Article> Articles { get; set; }

        [JsonProperty("external_resources")]
        public IList<ExternalResources> ExternalResources { get; set; }
    }

    public class Article : DWHResources
    {
        public Article()
        {
            ResType = "Article";
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }


        [JsonProperty("posted_date")]
        public string PostedDate { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("related_article_id")]
        public string RelatedArticleId { get; set; }
    }

    public class ExternalResources : DWHResources
    {
        public ExternalResources()
        {
            ResType = "ExternalResources";
        }

        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("link")]
        public string Link { get; set; }


        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }
    }

    public class DWHResources
    {
        [JsonProperty("title")]
        public string Title { get; set; }


        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonIgnore]
        public string ResType { get; set; }

        [JsonIgnore]
        public Command<DWHResources> OpenResourceCommand { get; set; }
    }

    public class Pages
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Settings
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    public class Country
    {
        [JsonProperty("iso")]
        public string ISO { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("printable_name")]
        public string PrintableName { get; set; }

        [JsonProperty("numcode")]
        public string NumCode { get; set; }
    }

    public static class ResourceExtension
    {
        public static void AddRange(this ObservableCollection<DWHResources> resources, IList<DWHResources> toAdd)
        {
            toAdd.ToList().ForEach(x =>
            {
                resources.Add(x);
            });
        }
    }
}
