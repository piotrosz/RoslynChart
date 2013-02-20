var CodeSample = (function () {
    function CodeSample(name, code) {
        this.name = name;
        this.code = code;
    }
    return CodeSample;
})();
var RoslynChartViewModel = (function () {
    function RoslynChartViewModel() {
        this.Samples = ko.observableArray([
            new CodeSample("Line chart", "code 1"), 
            new CodeSample("Bar chart", "code 2")
        ]);
        this.Editor = ace.edit("code");
        this.Editor.setTheme("ace/theme/monokai");
        this.Editor.getSession().setMode("ace/mode/csharp");
    }
    RoslynChartViewModel.prototype.getChart = function () {
        $.post("Chart/Create", "code=" + fixedEncodeURIComponent(this.Editor.getValue()), function (data) {
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
    return RoslynChartViewModel;
})();
function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str);
}
$(function () {
    ko.applyBindings(new RoslynChartViewModel());
});
