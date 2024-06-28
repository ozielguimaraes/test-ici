$(document).ready(function () {
    $(".select-pesquisa-local").select2({ language: "pt-BR" });
    if ($(".editor-html").length > 0) {
        $(".editor-html").summernote({
            lang: 'pt-BR',
            minHeight: 130,
            toolbar: [
                ['font', ['bold', 'underline', 'clear']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link']]
            ]
        });
    }
});
