var _htmlEditorId = "html-modal-editor";
var _jsOnModalOpenEditor = "js-on-modal-open-editor";
var _jsOnCGExecution = "js-on-cg-execution";
var _bindBeforeUnloadIsOn = false;
ace.require("ace/ext/language_tools");
ace.require("ace/ext/emmet");

$(document).ready(function () {
    cgComponent.setAreaForInsertingPageContainer("#preview-container");

    var htmlEditor = ace.edit(_htmlEditorId);
    htmlEditor.setTheme("ace/theme/monokai");
    htmlEditor.getSession().setMode("ace/mode/html");
    htmlEditor.setOptions(aceEditorOptions());

    var jsOnModalOpenEditor = ace.edit(_jsOnModalOpenEditor);
    jsOnModalOpenEditor.setTheme("ace/theme/monokai");
    jsOnModalOpenEditor.getSession().setMode("ace/mode/javascript");
    jsOnModalOpenEditor.setOptions(aceEditorOptions());

    var jsOnCGExecution = ace.edit(_jsOnCGExecution);
    jsOnCGExecution.setTheme("ace/theme/monokai");
    jsOnCGExecution.getSession().setMode("ace/mode/javascript");
    jsOnCGExecution.setOptions(aceEditorOptions());

    bindPageEvents(htmlEditor, jsOnModalOpenEditor, jsOnCGExecution);

    $('#html-collapse-link').click(function () {
        htmlEditor.focus();
    });

    $('#js-open-collapse-link').click(function () {
        jsOnModalOpenEditor.focus();
    });

    $('#js-exec-collapse-link').click(function () {
        jsOnCGExecution.focus();
    });
});

function aceEditorOptions() {
    var options = {};
    options.enableBasicAutocompletion = true;
    options.enableSnippets = true;
    options.enableEmmet = true;
    options.enableLiveAutoComplete = true;
    options.showPrintMargin = false;

    return options;
}

function bindPageEvents(htmlEditor, jsOnModalOpen, jsOnExec) {
    $(document).bind('keydown', function (e) {
        if (e.ctrlKey && (e.which == 83)) {
            e.preventDefault();
            save();

            return false;
        }
    });

    var bindUnload = function () {
        if (_bindBeforeUnloadIsOn == false) {
            $(window).on("beforeunload", function () {
                if (_bindBeforeUnloadIsOn == true) {
                    return "Are you sure you want to leave this page? Any unsaved changes will be lost!";
                }
            });
            _bindBeforeUnloadIsOn = true;
        }
    };

    $('#cg-tags').tagsInput({
        height: 35,
        defaultText: "Add Tag"
    });

    $('#more-options-div').click(function () {
        if ($('#more-options-show').is(":visible"))
        {
            $('#more-options-hide').show();
            $('#more-options-show').hide();
            $('#more-options-container').slideDown();
        }
        else {
            $('#more-options-show').show();
            $('#more-options-hide').hide();
            $('#more-options-container').slideUp();
        }
    });

    htmlEditor.on("change", function () {
        bindUnload();
    });

    jsOnModalOpen.on("change", function () {
        bindUnload();
    });

    jsOnExec.on("change", function () {
        bindUnload();
    });

    $('input[type="text"], input[type="checkbox"], textarea').on("change", function () {
        bindUnload();
    });

    $('#cg-name').on("keyup", function () {
        if ($.trim($(this).val()) == "") {
            $('#cg-edit-link').html("<span class='glyphicon glyphicon-pencil'></span> [Untitled]");
            return;
        }
        $('#cg-edit-link').html("<span class='glyphicon glyphicon-pencil'></span> " + $(this).val());
    });
}



function closePreview() {
    $('#preview-outer-container').hide();
    cgComponent.resetEverything();
}

function preview() {
    $('#preview-outer-container').hide();
    var htmlEditor = ace.edit(_htmlEditorId);
    var jsOnModalOpenEditor = ace.edit(_jsOnModalOpenEditor);
    var jsOnCGExecution = ace.edit(_jsOnCGExecution);

    var options = {};
    options.ModalHTML = htmlEditor.getValue();
    options.ModalName = $('#cg-name').val();
    options.OnModalOpenJS = jsOnModalOpenEditor.getValue();
    options.OnExecuteCGJS = jsOnCGExecution.getValue();
    options.OnAfterExecuteCGJS = function () {
        $('#preview-outer-container').fadeIn();
    };

    cgComponent.resetEverything();
    cgComponent.initiate(options);
    cgComponent.start();
}

function save() {
    showSaveNotification();

    var htmlEditor = ace.edit(_htmlEditorId);
    var jsOnModalOpenEditor = ace.edit(_jsOnModalOpenEditor);
    var jsOnCGExecution = ace.edit(_jsOnCGExecution);

    var data = {};
    data.CodeGenerationID = $('#code-generation-id').val();
    data.CGName = $('#cg-name').val();
    data.HTMLThatGoesInModal = htmlEditor.getValue();
    data.ScriptThatExecutesOnModalLoad = jsOnModalOpenEditor.getValue();
    data.ScriptThatExecutesUponButtonPress = jsOnCGExecution.getValue();
    data.CGDescription = $('#cg-description').val();
    data.Published = $('#cg-publish').is(":checked");

    var url = "/CodeGeneration/Save";
    $.post(url, { "componentModelString" : JSON.stringify(data) }, function (result) {
        $('#code-generation-id').val(result);
        hideSaveNotification();
        _bindBeforeUnloadIsOn = false;
    });
}

function showSaveNotification() {
    $('#save-notification').fadeIn();
}

function hideSaveNotification() {
    $('#save-notification').fadeOut();
}