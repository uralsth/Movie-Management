(function () {
    var ManageGenre = function () {
        var GenreManager = {
            config: {
                currentPage: 0,
                limit: 10,
                baseURL: '/dashboard/GenreNew/'
            },
            init: function () {
                this.getData(0, this.config.limit, 0);
                this.eventListener();
            },
            eventListener: function () {
                let me = this;
                $('#btnAddGenre').off('click').on('click', function () {
                    $('#GenreGrid').hide();
                    $('#GenreForm').show();
                    me.resetForm();
                });
                $('#btnBackToGrid').off('click').on('click', function () {
                    $('#GenreGrid').show();
                    $('#GenreForm').hide();
                });
                $('#btnSaveGenre').off('click').on('click', function () {
                    me.saveGenre();
                });
                $('.btnDeleteGenre').off('click').on('click', function (event) {
                    var id = $(event.target).data('id');
                    me.deleteGenre(id);
                });
            },

            resetForm: function () {
                $('#hdnGenreID').val(0);
                $('#txtGenre').val('');
                $('#txtGenreDescription').val('');
            },
            saveGenre: function () {
                let me = this;
                var myData = {
                    GenreID: $('#hdnGenreID').val(),
                    GenreName: $('#txtGenreName').val(),
                    Description: $('#txtGenreDescription').val()
                };
                var config = {
                    type: 'POST',
                    url: this.config.baseURL + 'Create',
                    data: JSON.stringify(myData),
                    success: function (response) {
                        if (response.isSucess) {
                            $('#GenreGrid').show();
                            $('#GenreForm').hide();
                            me.getData(0, me.config.limit, me.config.currentPage);
                        } else {
                            alert(response.errorMessage.join(','));
                        }
                    }
                };
                AjaxCall(config);
            },
            //editGenre: function () {
            //    let me = this;
            //    var id = $('#btnEditGenre').data('id');
            //    var config = {
            //        type: 'GET',
            //        url: this.config.baseURL + 'Edit' + id,
            //        data: {id:id},
            //        success: function (data) {
            //            $('#GenreGrid').hide();
            //            $('#GenreForm').show();
            //        }
            //    };
            //},
            deleteGenre: function (id) {
                let me = this;
                //var row = $(e.target).parent();
                var config = {
                    type: 'GET',
                    url: this.config.baseURL + 'Delete',
                    data: {id: id},
                    success: function (response) {
                        if (response.isSucess) {
                            me.getData(0, me.config.limit, me.config.currentPage);
                            $('tr[data-id=' + id + ']').remove();
                        }   else {
                            alert(response.errorMessage.join(','));
                        }
                    }
                };
                AjaxCall(config);
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
                    tr += `<td>${v.genreName}</td>`;
                    tr += `<td>${v.description}</td>`;
                    tr += `<td>${v.addedOn}</td>`;
                    tr += `<td>${v.addedBy}</td>`;
                    tr += `<td>
                                <button data-id="${v.genreID}" class="btn btn-primary btn-sm btnEdit">Edit</button>
                                <button data-id="${v.genreID}" class="btn btn-danger btn-sm btnDeleteGenre">Delete</button>
                            </td>`;
                    tr += '</tr>';
                });
                $('#tblBodyGenre').html(tr);
            }
        };
        GenreManager.init();
    }
    $.fn.ManageGenre = function () {
        ManageGenre($(this));
    }
})();