using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Collections;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Resolvers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Drawing;
using System.IO;
using Emby.Notification.Slack.Configuration;

namespace Emby.Notification.Slack
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages, IHasThumbImage
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        /// <summary>
        /// Gets the name of the plugin
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get { return "Slack Notifications"; }
        }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            
            return new[]
            {
                new PluginPageInfo
                {
                    Name = Name,
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.config.html"

                }
            };
        }

        private Guid _id = new Guid("e7bb7839-a69f-409e-a26e-ad7e0fab31cd");
        public override Guid Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description
        {
            get
            {
                return "Sends notifications to Slack and other webhook compatible systems.";
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Plugin Instance { get; private set; }

        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".thumb.png");
        }

        public ImageFormat ThumbImageFormat
        {
            get
            {
                return ImageFormat.Png;
            }
        }
    }
}
