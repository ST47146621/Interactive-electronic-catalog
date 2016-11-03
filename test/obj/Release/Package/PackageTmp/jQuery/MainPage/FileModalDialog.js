
function FileModalDialog(addFileModal, editFileModal, fileGrid) {
    var that = this;
    var uploadLimit = 15000000;

    this.do_InitFileModal = function () {
        addFileModal.dataEditModal();
        editFileModal.dataEditModal();

        addFileModal.on("shown.bs.modal", function () {
            $("#sendFileBtn", addFileModal).removeAttr("disabled");
        });

        $("#sendFileBtn", addFileModal).click(function () {
            $(this).attr("disabled", "disabled");
            var $form = $("form", addFileModal);

            if ($("#size", $form).val() * 1 > uploadLimit) {
                //TODO: 多國語言
                $.openErrorNoty("檔案過大無法上傳!");
                $(this).removeAttr("disabled");
                e.preventDefault();
                return false;
            }

            if ($form.valid()) {
                $form.ajaxSubmit({
                    url: $form.attr("action"),
                    dataType: "json",
                    contentType: "multipart/form-data",
                    success: function (response) {
                        isChange_Modal = false;
                        addFileModal.one('hidden.bs.modal', function (e) {
                            $form[0].reset();
                            $("input,textarea", $form).attr("disabled", "disabled");
                            gridTable.do_SetGridData(fileGrid, response.data);
                        }).modal("hide");
                    },
                    error: function (xhr) {
                        var noty = $.openAjaxErrorNoty(xhr);
                    }
                });
            }
        })

        $(".uploadFile", addFileModal).change(function (e) {
            var get_file = $(this)[0].files[0];

            var size = get_file.size;
            var fileInfo = get_file.name.split(".");
            var fileName = fileInfo[0];
            var extension = fileInfo[1];

            $("#name", addFileModal).val(fileName);
            $("#extension", addFileModal).val(extension);
            $("#size", addFileModal).val(size);
        });
    }

    this.do_EditFileModalError = function () {
        $.openAjaxErrorNoty(xhr);
        isChange_Modal = false;
        editFileModal.modal("hide");
    }

    this.do_EditFileModalOK = function () {
        $("#saveFileBtn", editFileModal).bind("click", function () {
            var form = $("form", editFileModal);
            if (form.valid()) {
                form.submit();
            }
            else {
                $(".input-validation-error:eq(0)", form).focus();
            }
        });
    }

    this.do_EditFileModalSaveSucess = function (response) {
        $.openSucessNoty();
        isChange_Modal = false;
        $("form", editFileModal).remove();
        editFileModal.one('hidden.bs.modal', function (e) {
            var data = response.data;
            var old_tr = fileGrid.find(".td_FileId:contains('" + data[0]["FileId"] + "')").closest("tr");
            var index = $(".tr", fileGrid).index(old_tr);
            old_tr.remove();
            gridTable.do_SetGridData(fileGrid, data, {
                complete: function ($this) {
                    if (index > 0) {
                        $this.find(".tr:last").insertAfter($this.find(".tr").eq(index - 1));
                    }
                    else {
                        $this.find(".tr:last").prependTo($this.find(".table_scrollBody tbody"));
                    }
                }
            });
        }).modal("hide");
    }
}