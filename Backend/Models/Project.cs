﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackendFramework.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("semanticDomains")]
        public List<SemanticDomain> SemanticDomains { get; set; }

        [BsonElement("vernacularWritingSystem")]
        public string VernacularWritingSystem { get; set; }

        [BsonElement("analysisWritingSystems")]
        public List<string> AnalysisWritingSystems { get; set; }

        [BsonElement("validCharacters")]
        public List<string> ValidCharacters { get; set; }

        [BsonElement("rejectedCharacters")]
        public List<string> RejectedCharacters { get; set; }

        [BsonElement("autocompleteSetting")]
        [BsonRepresentation(BsonType.String)]
		public AutocompleteSetting AutocompleteSetting { get; set; }

        /// <summary> Not implemented: optional fields for projects </summary>
        [BsonElement("customFields")]
        public List<CustomField> CustomFields { get; set; }

        /// <summary> Not implemented: optional fields for words in a project </summary>
        [BsonElement("wordFields")]
        public List<string> WordFields { get; set; }

        [BsonElement("partsOfSpeech")]
        public List<string> PartsOfSpeech { get; set; }

        public Project()
        {
            Id = "";
            Name = "";
            VernacularWritingSystem = "";
            SemanticDomains = new List<SemanticDomain>();
            AnalysisWritingSystems = new List<string>();
            ValidCharacters = new List<string>();
            RejectedCharacters = new List<string>();
            CustomFields = new List<CustomField>();
            WordFields = new List<string>();
            PartsOfSpeech = new List<string>();
        }

        public Project Clone()
        {
            var clone = new Project
            {
                Id = Id.Clone() as string,
                Name = Name.Clone() as string,
                VernacularWritingSystem = VernacularWritingSystem.Clone() as string,
                SemanticDomains = new List<SemanticDomain>(),
                AnalysisWritingSystems = new List<string>(),
                ValidCharacters = new List<string>(),
                RejectedCharacters = new List<string>(),
                CustomFields = new List<CustomField>(),
                WordFields = new List<string>(),
                PartsOfSpeech = new List<string>()
            };

            foreach (var sd in SemanticDomains)
            {
                clone.SemanticDomains.Add(sd.Clone());
            }
            foreach (var aws in AnalysisWritingSystems)
            {
                clone.AnalysisWritingSystems.Add(aws.Clone() as string);
            }
            foreach (var cs in ValidCharacters)
            {
                clone.ValidCharacters.Add(cs.Clone() as string);
            }
            foreach (var cs in RejectedCharacters)
            {
                clone.RejectedCharacters.Add(cs.Clone() as string);
            }
            foreach (var cf in CustomFields)
            {
                clone.CustomFields.Add(cf.Clone());
            }
            foreach (var wf in WordFields)
            {
                clone.WordFields.Add(wf.Clone() as string);
            }
            foreach (var pos in PartsOfSpeech)
            {
                clone.PartsOfSpeech.Add(pos.Clone() as string);
            }

            return clone;
        }

        public bool ContentEquals(Project other)
        {
            return
                other.Name.Equals(Name) &&
                other.VernacularWritingSystem.Equals(VernacularWritingSystem) &&

                other.SemanticDomains.Count == SemanticDomains.Count &&
                other.SemanticDomains.All(SemanticDomains.Contains) &&

                other.AnalysisWritingSystems.Count == AnalysisWritingSystems.Count &&
                other.AnalysisWritingSystems.All(AnalysisWritingSystems.Contains) &&

                other.ValidCharacters.Count == ValidCharacters.Count &&
                other.ValidCharacters.All(ValidCharacters.Contains) &&

                other.RejectedCharacters.Count == RejectedCharacters.Count &&
                other.RejectedCharacters.All(RejectedCharacters.Contains) &&

                other.CustomFields.Count == CustomFields.Count &&
                other.CustomFields.All(CustomFields.Contains) &&

                other.WordFields.Count == WordFields.Count &&
                other.WordFields.All(WordFields.Contains) &&

                other.PartsOfSpeech.Count == PartsOfSpeech.Count &&
                other.PartsOfSpeech.All(PartsOfSpeech.Contains);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var other = obj as Project;
                return other.Id.Equals(Id) && ContentEquals(other);
            }
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(SemanticDomains);
            hash.Add(VernacularWritingSystem);
            hash.Add(AnalysisWritingSystems);
            hash.Add(ValidCharacters);
            hash.Add(RejectedCharacters);
            hash.Add(CustomFields);
            hash.Add(WordFields);
            hash.Add(PartsOfSpeech);
            return hash.ToHashCode();
        }
    }

    public class CustomField
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public CustomField Clone()
        {
            return new CustomField
            {
                Name = Name.Clone() as string,
                Type = Type.Clone() as string
            };
        }
    }

    public class SemanticDomainWithSubdomains
    {
        public string Name;
        public string Id;
        public string Description;
        public List<SemanticDomainWithSubdomains> Subdomains;

        public SemanticDomainWithSubdomains(SemanticDomain sd)
        {
            Name = sd.Name;
            Id = sd.Id;
            Description = sd.Description;
            Subdomains = new List<SemanticDomainWithSubdomains>();
        }
    }

	public class ProjectWithUser : Project
	{
		public User UpdatedUser;

		public ProjectWithUser() { }

		public ProjectWithUser(Project baseObj)
		{
			Id = baseObj.Id;
			Name = baseObj.Name;
			PartsOfSpeech = baseObj.PartsOfSpeech;
			RejectedCharacters = baseObj.RejectedCharacters;
			SemanticDomains = baseObj.SemanticDomains;
			VernacularWritingSystem = baseObj.VernacularWritingSystem;
			WordFields = baseObj.WordFields;
			AnalysisWritingSystems = baseObj.AnalysisWritingSystems;
			CustomFields = baseObj.CustomFields;
			ValidCharacters = baseObj.ValidCharacters;
		    AutocompleteSetting = baseObj.AutocompleteSetting;
		}
	}

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AutocompleteSetting
    {
        Off,
        OnRequest,
        AlwaysOn
    }
}
