/// <reference path="typings/ace/ace.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap/bootstrap.d.ts" />

var editor;
var viewModel: RoslynChartViewModel;

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

        viewModel.getChart();
    }
};

class RoslynChartViewModel {

    public CodeSamples: KnockoutObservableAny;
    public Section: KnockoutObservableAny;
    public SubSection: KnockoutObservableAny;
    public CodeSample: KnockoutObservableAny;
    
    public getChart: Function;

    constructor() {

        var codeSamples;

        $.ajax({
            url: "Chart/GetCodeSamples",
            async: false,
            dataType: "json",
            success: function (data) {
                codeSamples = data;
            }
        });

        this.CodeSamples = ko.observable(codeSamples);

        this.Section = ko.observable(codeSamples[0]);
        this.SubSection = ko.observable(codeSamples[0].Sections[0]);
        this.CodeSample = ko.observable(codeSamples[0].Sections[0].CodeSamples[0]);

        this.getChart = () => {

            if (editor == undefined) {
                return;
            }

            $.post("Chart/Create",
            "code=" + fixedEncodeURIComponent(editor.getSession().getValue()),
            function (data) {
                if (data.Message == "Success") {
                    $("#img-chart").attr("src", "/Chart/ReturnChart?guid=" + data.Guid);

                    $("#messages").removeClass("alert alert-error").addClass("alert alert-success");
                    $("#messages").text('Success');
                }
                else {
                    $("#messages").removeClass("alert alert-success").addClass("alert alert-error");
                    $("#messages").text(data.Message);
                }
            });
        };
    }
}

// Encodes C# code from the editor
function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str);
}

$(function () {
    viewModel = new RoslynChartViewModel();
    ko.applyBindings(viewModel);
});