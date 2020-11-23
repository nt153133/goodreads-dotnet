﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Extensions;
using Newtonsoft.Json;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a single book as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Book : ApiResponse
    {
        /// <summary>
        /// The Goodreads Id for this book.
        /// </summary>
        [JsonProperty]
        public long Id { get; private set; }

        /// <summary>
        /// The title of this book.
        /// </summary>
        [JsonProperty]
        public string Title { get; private set; }

        /// <summary>
        /// The description of this book.
        /// </summary>
        [JsonProperty]
        public string Description { get; private set; }

        /// <summary>
        /// The ISBN of this book.
        /// </summary>
        [JsonProperty]
        public string Isbn { get; private set; }

        /// <summary>
        /// The ISBN13 of this book.
        /// </summary>
        [JsonProperty]
        public string Isbn13 { get; private set; }

        /// <summary>
        /// The ASIN of this book.
        /// </summary>
        [JsonProperty]
        public string Asin { get; private set; }

        /// <summary>
        /// The Kindle ASIN of this book.
        /// </summary>
        [JsonProperty]
        public string KindleAsin { get; private set; }

        /// <summary>
        /// The marketplace Id of this book.
        /// </summary>
        public string MarketplaceId { get; private set; }

        /// <summary>
        /// The country code of this book.
        /// </summary>
        public string CountryCode { get; private set; }

        /// <summary>
        /// The cover image for this book.
        /// </summary>
        [JsonProperty]
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The small cover image for this book.
        /// </summary>
        [JsonProperty]
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// The date this book was published.
        /// </summary>
        [JsonProperty]
        public DateTime? PublicationDate { get; private set; }

        /// <summary>
        /// The publisher of this book.
        /// </summary>
        public string Publisher { get; private set; }

        /// <summary>
        /// The language code of this book.
        /// </summary>
        public string LanguageCode { get; private set; }

        /// <summary>
        /// Signifies if this is an eBook or not.
        /// </summary>
        public bool IsEbook { get; private set; }

        /// <summary>
        /// The average rating of this book by Goodreads users.
        /// </summary>
        public decimal AverageRating { get; private set; }

        /// <summary>
        /// The number of pages in this book.
        /// </summary>
        public int Pages { get; private set; }

        /// <summary>
        /// The format of this book.
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Brief information about this edition of the book.
        /// </summary>
        public string EditionInformation { get; private set; }

        /// <summary>
        /// The count of all Goodreads ratings for this book.
        /// </summary>
        public int RatingsCount { get; private set; }

        /// <summary>
        /// The count of all reviews that contain text for this book.
        /// </summary>
        public int TextReviewsCount { get; private set; }

        /// <summary>
        /// The Goodreads Url for this book.
        /// </summary>
        [JsonProperty]
        public string Url { get; private set; }

        /// <summary>
        /// The aggregate information for this work across all editions of the book.
        /// </summary>
        public Work Work { get; private set; }

        /// <summary>
        /// The list of authors that worked on this book.
        /// </summary>
        [JsonProperty]
        public IReadOnlyList<AuthorSummary> Authors { get; private set; }

        /// <summary>
        /// HTML and CSS for the Goodreads iFrame. Used to display the reviews for this book.
        /// </summary>
        public string ReviewsWidget { get; private set; }

        /// <summary>
        /// The most popular shelf names this book appears on. This is a
        /// dictionary of shelf name -> count.
        /// </summary>
        public IReadOnlyDictionary<string, int> PopularShelves { get; private set; }

        /// <summary>
        /// The list of book links tracked by Goodreads.
        /// This is usually a list of libraries that the user can borrow the book from.
        /// </summary>
        public IReadOnlyList<BookLink> BookLinks { get; private set; }

        /// <summary>
        /// The list of buy links tracked by Goodreads.
        /// This is usually a list of third-party sites that the
        /// user can purchase the book from.
        /// </summary>
        public IReadOnlyList<BookLink> BuyLinks { get; private set; }

        /// <summary>
        /// Summary information about similar books to this one.
        /// </summary>
        public IReadOnlyList<BookSummary> SimilarBooks { get; private set; }

        /// <summary>
        /// Summary information about similar books to this one.
        /// </summary>
        //[JsonProperty]
        public IReadOnlyList<SeriesWork> Series { get; private set; }

        [JsonProperty]
        public IReadOnlyList<long> SeriesIds { get; private set; }

        // TODO: parse series information once I get a better sense
        // of what series are from the other API calls.
        //// public List<Series> Series { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Book: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title");
            Isbn = element.ElementAsString("isbn");
            Isbn13 = element.ElementAsString("isbn13");
            Asin = element.ElementAsString("asin");
            KindleAsin = element.ElementAsString("kindle_asin");
            MarketplaceId = element.ElementAsString("marketplace_id");
            CountryCode = element.ElementAsString("country_code");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            PublicationDate = element.ElementAsMultiDateField("publication");
            Publisher = element.ElementAsString("publisher");
            LanguageCode = element.ElementAsString("language_code");
            IsEbook = element.ElementAsBool("is_ebook");
            Description = element.ElementAsString("description");
            AverageRating = element.ElementAsDecimal("average_rating");
            Pages = element.ElementAsInt("num_pages");
            Format = element.ElementAsString("format");
            EditionInformation = element.ElementAsString("edition_information");
            RatingsCount = element.ElementAsInt("ratings_count");
            TextReviewsCount = element.ElementAsInt("text_reviews_count");
            Url = element.ElementAsString("url");
            ReviewsWidget = element.ElementAsString("reviews_widget");

            var workElement = element.Element("work");
            if (workElement != null)
            {
                Work = new Work();
                Work.Parse(workElement);
            }

            Authors = element.ParseChildren<AuthorSummary>("authors", "author");
            SimilarBooks = element.ParseChildren<BookSummary>("similar_books", "book");
            Series = element.ParseChildren<SeriesWork>("series_works", "series_work");
            var bookLinks = element.ParseChildren<BookLink>("book_links", "book_link");
            if (bookLinks != null)
            {
                bookLinks.ForEach(x => x.FixBookLink(Id));
                BookLinks = bookLinks;
            }

            var buyLinks = element.ParseChildren<BookLink>("buy_links", "buy_link");
            if (buyLinks != null)
            {
                buyLinks.ForEach(x => x.FixBookLink(Id));
                BuyLinks = buyLinks;
            }

            var shelves = element.ParseChildren(
                "popular_shelves",
                "shelf",
                (shelfElement) =>
            {
                var shelfName = shelfElement?.Attribute("name")?.Value;
                var shelfCountValue = shelfElement?.Attribute("count")?.Value;

                int shelfCount = 0;
                int.TryParse(shelfCountValue, out shelfCount);
                return new KeyValuePair<string, int>(shelfName, shelfCount);
            });

            if (shelves != null)
            {
                PopularShelves = shelves.GroupBy(obj => obj.Key).ToDictionary(shelf => shelf.Key, shelf => shelf.Sum(x => x.Value));
            }

            if (Series != null && Series.Any())
            {
                SeriesIds = Series.Select(i => i.Series.Id).ToList();
            }
            else
            {
                SeriesIds = new List<long>();
            }
        }
    }
}
