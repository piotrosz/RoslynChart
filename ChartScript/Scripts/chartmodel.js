var editor;
ko.bindingHandlers["code"] = {
    init: function (element) {
        editor = ace.edit("code");
        editor.setTheme("ace/theme/monokai");
        editor.getSession().setMode("ace/mode/csharp");
    },
    update: function (element, valueAccessor) {
        var currentValue = valueAccessor();
        editor.setValue(currentValue);
    }
};
var CodeSample = (function () {
    function CodeSample(Name, Code) {
        this.Name = Name;
        this.Code = Code;
    }
    return CodeSample;
})();
var RoslynChartViewModel = (function () {
    function RoslynChartViewModel() {
        var _this = this;
        var codeSamples;
        $.ajax({
            url: "Chart/CodeSamples",
            async: false,
            dataType: "json",
            success: function (data) {
                codeSamples = data;
            }
        });
        this.CodeSamples = ko.observableArray(codeSamples);
        this.CodeSample = ko.observable(codeSamples[1]);
        this.getChart = function () {
            $.post("Chart/Create", "code=" + fixedEncodeURIComponent(_this.CodeSample().Code), function (data) {
                if(data.Message == "Success") {
                    $("#img-chart").attr("src", "/Chart/ReturnChart?guid=" + data.Guid);
                    $("#messages").removeClass("alert alert-error").addClass("alert alert-success");
                    $("#messages").text('Success');
                } else {
                    $("#messages").removeClass("alert alert-success").addClass("alert alert-error");
                    $("#messages").text(data.Message);
                }
            });
        };
    }
    return RoslynChartViewModel;
})();
function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str);
}
$(function () {
    ko.applyBindings(new RoslynChartViewModel());
});
