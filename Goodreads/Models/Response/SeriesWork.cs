using System.Collections.Generic;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    public class SeriesWork : ApiResponse
    {
        /// <summary>
        /// The Id of this book.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The UserPosition of this book.
        /// </summary>
        public long UserPosition { get; private set; }

        /// <summary>
        /// The list of book links tracked by Goodreads.
        /// This is usually a list of libraries that the user can borrow the book from.
        /// </summary>
        public Series Series { get; private set; }
        
        

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            UserPosition = element.ElementAsLong("user_position");
            var workElement = element.Element("series");
            Series = new Series();
            Series.Parse(workElement);
            //SeriesList = element.ParseChildren<Series>("series_works", "series");
        }
    }
}
