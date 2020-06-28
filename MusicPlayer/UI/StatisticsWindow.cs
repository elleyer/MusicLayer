using GeniusAPIWrapper.JsonData.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MusicPlayer.UI
{
    class StatisticsWindow
    {
        private Dispatcher _dispatcher;

        private List<TextBlock> TextBlocks = new List<TextBlock>();

        private TextBlock _fullTitleBox, _resultIdBox, _lyricsOwnerIdBox, _remotePath, _title,
            _titleWithFeature, _url, _artistName, _artistUrl, _artistImageUrl,
            _artistId, _isArtistVerified, _unreviewedAnnotations,
            _concurrents, _isHot, _pageViews, _lyrics;

        //We need to impore code quality here because it's really sucks.

        internal StatisticsWindow(Dispatcher dispatcher, TextBlock fullTitleBox, TextBlock resultIdBox, TextBlock
            lyricsOwnerIdBox, TextBlock remotePath, TextBlock title, TextBlock titleWithFeature,
            TextBlock url, TextBlock artistName, TextBlock artistUrl, TextBlock artistImageUrl,
            TextBlock artistId, TextBlock IsArtistVerified, TextBlock unreviewedAnnotations,
            TextBlock concurrents, TextBlock isHot, TextBlock pageViews, TextBlock lyrics)
        {
            _dispatcher = dispatcher;

            _fullTitleBox = fullTitleBox;
            _resultIdBox = resultIdBox;
            _lyricsOwnerIdBox = lyricsOwnerIdBox;
            _remotePath = remotePath;
            _title = title;
            _titleWithFeature = titleWithFeature;
            _url = url;
            _artistName = artistName;
            _artistUrl = artistUrl;
            _artistImageUrl = artistImageUrl;
            _artistId = artistId;
            _isArtistVerified = IsArtistVerified;
            _unreviewedAnnotations = unreviewedAnnotations;
            _concurrents = concurrents;
            _isHot = isHot;
            _pageViews = pageViews;
            _lyrics = lyrics;

            TextBlocks.AddRange(new[] { _fullTitleBox, _resultIdBox, _lyricsOwnerIdBox, _remotePath, _title, 
                _titleWithFeature,_url, _artistName, _artistUrl, _artistImageUrl, _artistId, _isArtistVerified,
                _unreviewedAnnotations, _concurrents, _isHot, _pageViews, _lyrics});
        }

        internal void Update(SearchResponse response)
        {
            try
            {
                _dispatcher.Invoke(() =>
                {
                    var hit = response.Response.Hits[0];

                    _lyrics.Text = response.Lyrics;
                    _lyrics.UpdateLayout();

                    _fullTitleBox.Text = "Full title: " + hit.Result.FullTitle;

                    _resultIdBox.Text = "Id: " + hit.Result.Id.ToString();

                    _lyricsOwnerIdBox.Text = "Owner Id: " + hit.Result.PrimaryArtist.Id.ToString();
                    _remotePath.Text = "Remote path: " + hit.Result.Path.ToString();
                    _title.Text = "Title: " + hit.Result.Title.ToString();
                    _titleWithFeature.Text = "Title With Feature: " + hit.Result.TitleWithFeatured.ToString();
                    _url.Text = "Url: " + hit.Result.Url.ToString();
                    _artistName.Text = "Artist name: " + hit.Result.PrimaryArtist.Name;
                    _artistUrl.Text = "Artist Url: " + hit.Result.PrimaryArtist.Url;
                    _artistImageUrl.Text = "Artist Image Url: " + hit.Result.PrimaryArtist.ImageUrl;
                    _artistId.Text = "Artist Id: " + hit.Result.PrimaryArtist.Id.ToString();
                    _isArtistVerified.Text = "Is artist verified: " + hit.Result.PrimaryArtist.IsVerified.ToString();
                    _unreviewedAnnotations.Text = "Unreviewed annotations: " + hit.Result.Stats.UnreviewedAnnotations.ToString();
                    _concurrents.Text = "Concurrents: " + hit.Result.Stats.Concurrents.ToString();
                    _isHot.Text = "Is Hot: " + hit.Result.Stats.Hot.ToString();
                    _pageViews.Text = "Page views: " + hit.Result.Stats.PageViews.ToString();

                    Console.WriteLine(_lyrics.Text);
                });
            }

            catch (Exception ex)
            {
                TextBlocks.ForEach(x => x.Text = "unknown...");
                Console.WriteLine(ex.ToString());

                return;
            }
        }
    }
}
