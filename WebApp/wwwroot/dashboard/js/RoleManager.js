(function () {
    var ManageRole = function () {
        var RoleManager = {
            config: {
                currentPage: 0,
                limit: 10,
                baseURL: '/dashboard/RoleNew/'
            },
            init: function () {
                this.getData(0, this.config.limit, 0);
                this.eventListener();
            },
            eventListener: function () {
                let me = this;
                $('#btnAddRole').off('click').on('click', function () {
                    $('#RoleGrid').hide();
                    $('#RoleForm').show();
                    me.resetForm();
                });
                $('#btnBackToGrid').off('click').on('click', function () {
                    $('#RoleGrid').show();
                    $('#RoleForm').hide();
                });
                $('#btnSaveRole').off('click').on('click', function () {
                    me.saveRole();
                });
                $('.btnDeleteRole').off('click').on('click', function (event) {
                    var id = $(event.target).data('id');
                    me.deleteRole(id);
                });
            },

            resetForm: function () {
                $('#hdnRoleID').val(0);
                $('#txtRole').val('');
                $('#txtRoleDescription').val('');
            },
            saveRole: function () {
                let me = this;
                var myData = {
                    RoleID: $('#hdnRoleID').val(),
                    RoleName: $('#txtRoleName').val(),
                    Description: $('#txtRoleDescription').val()
                };
                var config = {
                    type: 'POST',
                    url: this.config.baseURL + 'Create',
                    data: JSON.stringify(myData),
                    success: function (response) {
                        if (response.isSucess) {
                            $('#RoleGrid').show();
                            $('#RoleForm').hide();
                            me.getData(0, me.config.limit, me.config.currentPage);
                        } else {
                            alert(response.errorMessage.join(','));
                        }
                    }
                };
                AjaxCall(config);
            },
            //editRole: function () {
            //    let me = this;
            //    var id = $('#btnEditRole').data('id');
            //    var config = {
            //        type: 'GET',
            //        url: this.config.baseURL + 'Edit' + id,
            //        data: {id:id},
            //        success: function (data) {
            //            $('#RoleGrid').hide();
            //            $('#RoleForm').show();
            //        }
            //    };
            //},
            deleteRole: function (id) {
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
                    tr += `<td>${v.roleName}</td>`;
                    tr += `<td>${v.description}</td>`;
                    tr += `<td>${v.addedOn}</td>`;
                    tr += `<td>${v.addedBy}</td>`;
                    tr += `<td>
                                <button data-id="${v.roleID}" class="btn btn-primary btn-sm btnEdit">Edit</button>
                                <button data-id="${v.roleID}" class="btn btn-danger btn-sm btnDeleteRole">Delete</button>
                            </td>`;
                    tr += '</tr>';
                });
                $('#tblBodyRole').html(tr);
            }
        };
        RoleManager.init();
    }
    $.fn.ManageRole = function () {
        ManageRole($(this));
    }
})();
