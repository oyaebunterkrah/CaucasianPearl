using System;
using System.Web.Mvc;

namespace CaucasianPearl.Core.Filters
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShowAttribute : Attribute, IMetadataAware
    {
        /// <summary>
        /// ShowForDisplay.
        /// </summary>
        public bool ShowForDisplay { get; set; }
        /// <summary>
        /// ShowForEdit.
        /// </summary>
        public bool ShowForEdit { get; set; }

        public ShowAttribute()
        {
            ShowForDisplay = true;
            ShowForEdit = true;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            metadata.ShowForDisplay = ShowForDisplay;
            metadata.ShowForEdit = ShowForEdit;
        }
    }
}