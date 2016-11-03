
var isChange_Modal;

(function ($, undefined) {
    $.fn.dataEditModal = function () {
        $(this).each(function () {
            var self = $(this);
            if (self.hasClass("modal")) {

                self.on("shown.bs.modal", function () {
                    $("input,select,textarea", self).removeAttr("disabled");
                    isChange_Modal = false;

                    var $form = $("form", self).removeData("validator")
                        .removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse($form);
                });

                self.delegate("input,select,textarea", "change keydown", function () {
                    isChange_Modal = true;
                });

                self.on("hide.bs.modal", function () {
                    if (isChange_Modal) {
                        var $noty = $.openChangeConfirmNoty(self, function () {
                            $("input,select,textarea", self).attr("disabled", "disabled");
                            isChange_Modal = false;
                            self.modal("hide");
                        });
                        return false;
                    }
                });
            }
        });
    }

})(jQuery);