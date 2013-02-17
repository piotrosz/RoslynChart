// TODO: Use knockout

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