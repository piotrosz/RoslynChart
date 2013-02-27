/// <reference path="ace.d.ts" />
/// <reference path="knockout.d.ts" />
/// <reference path="jquery.d.ts" />
/// <reference path="bootstrap.d.ts" />

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

class CodeSample {
    constructor(public Name: string, public Code: string) {
    }
}

class RoslynChartViewModel {

    public CodeSamples: KnockoutObservableArray;
    public CodeSample: KnockoutObservableAny;
    
    public getChart: Function;

    constructor() {

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

        this.getChart = () => {

            $.post("Chart/Create",
            "code=" + fixedEncodeURIComponent(this.CodeSample().Code),
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
    return encodeURIComponent(str); //.replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
}

$(function () {
    ko.applyBindings(new RoslynChartViewModel());
});