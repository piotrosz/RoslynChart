var CodeSample = (function () {
    function CodeSample(name, code) {
        this.Name = name;
        this.Code = code;
    }
    return CodeSample;
})();

var CodeSampleList = (function () {

    // TODO: get code samples from server
    function CodeSampleList() {
        this.Samples = ko.observableArray([
            new CodeSample("Line chart", "code"),
            new CodeSample("Bar chart", "code")
        ]);
    }
    return CodeSampleList;
})();


// Encodes C# code from the editor
function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str).replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
}

$(function () {
    $("#btn-run").click(function () {
        $.post(urlDrawChart,
            "code=" + fixedEncodeURIComponent(editor.getValue()),
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
    });
});