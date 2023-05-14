(function () {
    var ManagePeople = function () {
        // private function
        var PeopleManager = {
            config: {
                currentPage: 0,
                limit: 10,
                baseURL: '/dashboard/PeopleNew/'
            },
            // method which is called initially
            init: function () {
                this.getData(0, this.config.limit, 1);
                this.eventListener();
            },
            eventListener: function () {
                let me = this;
                $('#btnAddInterest').off('click').on('click', function () {
                    $('#PeopleGrid').hide();
                    $('#PeopleForm').show();
                    me.resetForm();
                });
                $('#btnBackToGrid').off('click').on('click', function () {
                    $('#PeopleGrid').show();
                    $('#PeopleForm').hide();
                })
            },

            getData: function (offset, limit, currentPage) {
                let me = this;
                var config = {
                    type: 'GET',
                    url: this.config.baseURL + 'GetData',
                    data: {
                        offset: currentPage * limit,
                        limit: limit,
                        searckKeyword: $.trim($('#txtSearch').val())
                    },
                    success: function (data) {
                        me.bindDataToGrid(data);
                    }
                };
                AjaxCall(config);
            },
            resetForm: function () {

            },
            bindDataToGrid: function (data) {
                var tr = '';
                $.each(data, function (i, v) {
                    tr += '<tr>';
                    tr += `<td><img style="width:150px;" src="${v.imagePath}" class="img-thumbnail"></td>`;
                    tr += `<td>${v.gender}</td>`;
                    tr += `<td>${v.peopleName}</td>`;
                    tr += `<td>${v.dateOfBirth}</td>`;
                    tr += `<td>${v.addedOn}</td>`;
                    tr += `<td>${v.addedBy}</td>`;
                    tr += `<td>
                            <button class="btn btn-primary btn-sm btnEdit" data-id="${v.peopleID}">Edit</button>
                            <button class="btn btn-primary btn-sm btnDelete" data-id="${v.peopleID}">Delete</button>
                            </td>`;
                    tr += '</td>';
                });
                $('#tblBodyPeople').html(tr);
            }
        };
        PeopleManager.init();
    }
    $.fn.ManagePeople = function () {
        // $PeopleModule is this people
        // it calls private function
        ManagePeople($(this));
    }
})();
