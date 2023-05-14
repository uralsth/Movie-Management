(function () {
    var ManageMovie = function () {
        var MovieManager = {
            config: {
                currentPage: 0,
                limit: 10,
                baseURL: '/dashboard/MovieNew/'
            },

            init: function () {
                this.getData(0, this.config.limit, 0);
                this.eventListener();
            },
            eventListener: function () {
                let me = this;
                $('#btnAddMovie').off('click').on('click', function () {
                    $('#MovieGrid').hide();
                    $('#MovieForm').show();
                    me.resetForm();
                });
                $('#btnSaveMovie').off('click').on('click', function () {
                    me.saveMovie();
                });
                $('#btnBackToGrid').off('click').on('click', function () {
                    $('#MovieGrid').show();
                    $('#MovieForm').hide();
                });

            },

            resetForm: function () {
            },
            getData: function (offset, limit, currentPage) {
                let me = this;
                var config = {
                    type: 'GET',
                    url: this.config.baseURL + 'GetData',
                    data: {
                        offset: currentPage * limit,
                        limit: limit
                    },
                    success: function (data) {
                        me.bindDataToGrid(data);
                    }
                };
                AjaxCall(config);
            },
            bindDataToGrid: function (data) {
                var tr = '';
                $.each(data, function (i, v) {
                    tr += '<tr>';
                    tr += `<td><img style="width:150px;" src="${v.imagePath}" class="img-thumbnail"></td>`;
                    tr += `<td>${v.movieName}</td>`;
                    tr += `<td>${v.addedOn}</td>`;
                    tr += `<td>${v.addedBy}</td>`;
                    tr += `<td>
                                <button data-id="${v.movieID}" class="btn btn-primary btn-sm btnEdit">Edit</button>
                                <button data-id="${v.movieID}" class="btn btn-danger btn-sm btnDeleteMovie">Delete</button>
                            </td>`;
                    tr += '</tr>';
                });
                $('#tblBodyMovie').html(tr);
            },
            saveMovie: function () {
                let me = this;
                var selectedGenres = [];
                $("input:checkbox[name=GenreIDs]:checked").each(function () {
                    selectedGenres.push($(this).val());
                });
                var selectedActors = [];
                $("input:checkbox[name=ActorIDs]:checked").each(function () {
                    selectedActors.push($(this).val());
                });
                var selectedDirectors = [];
                $("input:checkbox[name=DirectorIDs]:checked").each(function () {
                    selectedDirectors.push($(this).val());
                });
                var selectedScreenwriters = [];
                $("input:checkbox[name=ScreenwriterIDs]:checked").each(function () {
                    selectedScreenwriters.push($(this).val());
                });
                var myData = {
                    MovieID: $('#hdnMovieID').val(),
                    MovieName: $('#txtMovieName').val(),
                    PlotSummary: $('#txtPlotSummary').val(),
                    ImagePath: $('#txtImagePath').val(),
                    ReleaseDate: $('#txtReleaseDate').val(),
                    Runtime: $('#txtRuntime').val(),
                    Language: $('#txtLanguage').val(),
                    TrailerLink: $('#txtTrailerLink').val(),
                    GenreIDs: selectedGenres,
                    PlatformIDs: selectedPlatforms,
                    ActorIDs: selectedActors,
                    DirectorIDs : selectedDirectors,
                    ScreenwriterIDs : selectedScreenwriters

                };
                var config = {
                    type: 'POST',
                    url: this.config.baseURL + 'Create',
                    data: JSON.stringify(myData),
                    success: function (response) {
                        if (response.isSucess) {
                            $('#MovieGrid').show();
                            $('#MovieForm').hide();
                            me.getData(-1, me.config.limit, me.config.currentPage);
                        } else {
                            alert(response.errorMessage.join(','));
                        }
                    }
                };
                AjaxCall(config);
            },
        };
        MovieManager.init();
    }
    $.fn.ManageMovie = function () {
        ManageMovie($(this));
    }
})();
