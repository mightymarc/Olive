window.AppController = Backbone.Controller.extend({
    _index: null,
    _photos: null,
    _album: null,
    _subalbums: null,
    _subphotos: null,
    _data: null,
    _photosview: null,
    _currentsub: null,

    routes: {
        "account/auth": "index",
        "subalbum/:id": "hashsub",
        "subalbum/:id/": "directphoto",
        "subalbum/:id/:num": "hashphoto"
    },

    initialize: function(options) {

        var ws = this;

        if (this._index === null) {
            $.ajax({
                url: 'data/album1.json',
                dataType: 'json',
                data: {},
                success: function(data) {
                    ws._data = data;
                    ws._photos = new PhotoCollection(data);
                    ws._index = new IndexView({ model: ws._photos });
                    Backbone.history.loadUrl();

                }
            });
            return this;
        }
        return this;
    },


    /**
    * Handle rendering the initial view for the application
    * @type function
    */
    index: function() {
        this._index.render();
    },


    /**
    * Gallery -> hashsub: Handle URL routing for subalbums. As subalbums aren't traversed 
    * in the default initialization of the app, here we create a new PhotoCollection for a 
    * particular subalbum based on indices passed through the UI. We then create a new SubalbumView 
    * instance, render the subalbums and set the current subphotos array to contain our subalbum Photo 
    * items. All of this is cached using the CacheProvider we defined earlier
    * @type function
    * @param {String} id An ID specific to a particular subalbum based on CIDs
    */
    hashsub: function(id) {

        var properindex = id.replace('c', '');
        this._currentsub = properindex;
        this._subphotos = cache.get('pc' + properindex) || cache.set('pc' + properindex, new PhotoCollection(this._data[properindex].subalbum));
        this._subalbums = cache.get('sv' + properindex) || cache.set('sv' + properindex, new SubalbumView({ model: this._subphotos }));
        this._subalbums.render();


    },

    directphoto: function(id) {

    },

    /**
    * Gallery -> hashphoto: Handle routing for access to specific images within subalbums. This method 
    * checks to see if an existing subphotos object exists (ie. if we've already visited the 
    * subalbum before). If it doesn't, we generate a new PhotoCollection and finally create 
    * a new PhotoView to display the image that was being queried for. As per hashsub, variable/data 
    * caching is employed here too
    * @type function
    * @param {String} num An ID specific to a particular image being accessed
    * @param {Integer} id An ID specific to a particular subalbum being accessed
    */
    hashphoto: function(num, id) {
        this._currentsub = num;

        num = num.replace('c', '');

        if (this._subphotos == undefined) {
            this._subphotos = cache.get('pc' + num) || cache.set('pc' + num, new PhotoCollection(this._data[num].subalbum));
        }
        this._subphotos.at(id)._view = new PhotoView({ model: this._subphotos.at(id) });
        this._subphotos.at(id)._view.render();

    }
});
