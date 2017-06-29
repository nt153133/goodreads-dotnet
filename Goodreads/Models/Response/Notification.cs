﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about notifications.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Notification : ApiResponse
    {
        /// <summary>
        /// The Goodreads Notification Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// An user who made notification.
        /// </summary>
        public Actor User { get; protected set; }

        /// <summary>
        /// Determine whether notification is new.
        /// </summary>
        public bool New { get; protected set; }

        /// <summary>
        /// A notification created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        /// <summary>
        /// A notification text.
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// A notification html body.
        /// </summary>
        public string HtmlBody { get; protected set; }

        /// <summary>
        /// A notification URL.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// A notification resource type.
        /// </summary>
        public string ResourceType { get; protected set; }

        /// <summary>
        /// A notification group resource type.
        /// </summary>
        public string GroupResourceType { get; protected set; }

        /// <summary>
        /// The friend group resource.
        /// </summary>
        public FriendGroupResource Friend { get; protected set; }

        /// <summary>
        /// The read status group resource.
        /// </summary>
        public ReadStatus ReadStatus { get; protected set; }

        /// <summary>
        /// The author following group resource.
        /// </summary>
        public AuthorFollowingGroupResource AuthorFollowing { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. Text: {1}. Made by: {2}",
                    Id,
                    Text,
                    User?.DisplayName);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");

            var user = element.Element("actors")?.Element("user");
            if (user != null)
            {
                User = new Actor();
                User.Parse(user);
            }

            New = element.ElementAsBool("new");
            CreatedDateTime = element.ElementAsDateTime("created_at");

            var body = element.Element("body");
            Text = body?.ElementAsString("text");
            HtmlBody = body?.ElementAsString("html");
            Url = element.ElementAsString("url");
            ResourceType = element.ElementAsString("resource_type");
            GroupResourceType = element.ElementAsString("group_resource_type");

            var groupResource = element.Element("group_resource");

            var readStatus = groupResource.Element("read_status");
            if (readStatus != null)
            {
                ReadStatus = new ReadStatus();
                ReadStatus.Parse(readStatus);
            }

            var friend = groupResource.Element("friend");
            if (friend != null)
            {
                Friend = new FriendGroupResource();
                Friend.Parse(friend);
            }

            var authorFollowing = groupResource.Element("author_following");
            if (authorFollowing != null)
            {
                AuthorFollowing = new AuthorFollowingGroupResource();
                AuthorFollowing.Parse(authorFollowing);
            }
        }
    }
}
