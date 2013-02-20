/// <reference path="ace.d.ts" />
/// <reference path="knockout.d.ts" />
/// <reference path="jquery.d.ts" />
/// <reference path="bootstrap.d.ts" />

class CodeSample {
    constructor(public name: string, public code: string) {

    }
}

class RoslynChartViewModel {

    public Samples: KnockoutObservableArray;
    public Editor: any;


    constructor() {
        this.Samples = ko.observableArray([
            new CodeSample("Line chart", "code 1"),
            new CodeSample("Bar chart", "code 2")
        ]);

        this.Editor = ace.edit("code");
        this.Editor.setTheme("ace/theme/monokai");
        this.Editor.getSession().setMode("ace/mode/csharp");
    }

    getChart() {
        $.post("Chart/Create",
            "code=" + fixedEncodeURIComponent(this.Editor.getValue()),
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
    }
}

// Encodes C# code from the editor
function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str); //.replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
}

$(function () {
    ko.applyBindings(new RoslynChartViewModel());

    //$("#btn-run").click(function () {  
    //});
});