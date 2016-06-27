using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;

namespace BBS.BBRZ
{
    [Serializable]
    public class Replay
    {
        static XmlSerializer _serializer = null;
        public string ClientVersion { get; set; }


        public Replay()
        {
        }

        public static Replay CreateFromString(string txt)
        {
            if (_serializer == null)
                _serializer = new XmlSerializer(typeof(Replay));

            var settings = new XmlReaderSettings();
            settings.CheckCharacters = false;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None;
            settings.ValidationType = ValidationType.None;

            var root = XmlReader.Create(new StringReader(txt), settings);
            var r = (Replay)_serializer.Deserialize(root);

            return r;
        }

        /// <summary>
        /// all not null steps
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<ReplayStep> ValidSteps()
        {
            return this.ReplayStep.Where(rp => rp.RulesEventBoardAction != null);
        }

        public ReplayStep FirstStep
        {
            get { return ReplayStep.First(); }
        }

        public ReplayStep LastStep
        {
            get { return ReplayStep.Last(); }
        }

        [XmlElement]
        public List<ReplayStep> ReplayStep { get; set; }

    }
}

