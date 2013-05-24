(function($) {
    $(document).ready(function() {

    });
}(jQuery));

replaceByCKEditor = function(configPath, elementId, submitId) {
    CKEDITOR.config.customConfig = configPath;
    CKEDITOR.replace(elementId);
    CKEDITOR.instances[elementId].setData($('#' + elementId).val());

    $('#' + submitId).click(function() {
        $('#' + elementId).val(CKEDITOR.instances[elementId].getData());
    });
};