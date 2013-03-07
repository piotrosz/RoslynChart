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
        editor.getSession().getSelection().clearSelection();
    }
};
var RoslynChartViewModel = (function () {
    function RoslynChartViewModel() {
        var codeSamples;
        $.ajax({
            url: "Chart/CodeSamples",
            async: false,
            dataType: "json",
            success: function (data) {
                codeSamples = data;
            }
        });
        this.CodeSamples = ko.observable(codeSamples);
        this.Section1 = ko.observable(codeSamples[0]);
        this.Section2 = ko.observable(codeSamples[0].Sections[0]);
        this.CodeSample = ko.observable(codeSamples[0].Sections[0].CodeSamples[0]);
        this.getChart = function () {
            if(editor == undefined) {
                return;
            }
            $.post("Chart/Create", "code=" + fixedEncodeURIComponent(editor.getSession().getValue()), function (data) {
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
    var viewModel = new RoslynChartViewModel();
    ko.applyBindings(viewModel);
    viewModel.getChart();
});
