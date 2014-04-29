using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewDataSourceVirt
{
    class PhotosIncrementalLoader : IncrementalLoadingBase
    {
        private string[] tags = new string[] {
            "cats",
            "dogs",
            "puppies",
            "pythons",
            "elephants",
            "lions",
            "tigers",
            "hyenas",
            "snakes",
            "anacondas",
            "dinosaur",
            "velociraptor",
            "lake",
            "mountains",
            "hills",
            "babies"
        };

        private int loadIndex = -1;
        Random random = new Random();

        protected override bool HasMoreItemsOverride()
        {
            return loadIndex < tags.Length;
        }

        async protected override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            ++loadIndex;
            var pg = await PhotosDataSource.Search(tags[loadIndex], random.Next(5, 15));
            return new List<object> {
                pg
            };
        }
    }
}
