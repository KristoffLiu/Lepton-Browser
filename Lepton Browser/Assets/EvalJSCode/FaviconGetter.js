var FaviconGetter = new function () {
    var self = this;

    self.TryGetFavicon = function () {
        var iconList = document.head.querySelectorAll('link[rel=icon]');
        var length = iconList.length;
        if (length == 0)
        {
            iconList = document.head.querySelectorAll('link[rel="shortcut icon"]');
            length = iconList.length;
        }
            
        var reslutList = {
            favicon_urls: []
        };
        for (var i = 0; i < length; ++i) {
            reslutList.favicon_urls.push(iconList[i].getAttribute('href'));
        }

        if (window.faviconCrawller) {
            window.faviconCrawller.didCrawlFavicon(JSON.stringify(reslutList));
        }
    }
}