function do_BindMoneyFormat() {
    $(document).delegate(".fmoney", "change", function () {
        var $this = $(this), value, type, defaultValue;
        value = $this.val();
        type = $this.data("pattern");
        defaultValue = $this.data("defaultValue");

        var outPut = value.toDecimal(type, defaultValue);
        $this.data("oldvalue", value).val(outPut);
    })
}
