
function do_PowerCheck(power) {
    var p = false;

    if (typeof (_power) != "undefined")
        p = _power[power];
    else if (opener._power != null) {
        p = opener._power[power];
    }
    else {
        p = true;
    }

    if (!p) {
        //TODO 多國語言
        $.openErrorNoty("無使用本功能權限！", { dismissQueue: false, killer: true });
    }

    return p;
}