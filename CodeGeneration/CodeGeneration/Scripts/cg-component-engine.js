var cgComponent = function () {
    var _modalHtml = "";
    var _onModalOpenJS = "";
    var _onExecuteCGJS = "";
    var _modalName = "";
    var _onAfterExecuteCGJS = null;
    var _defaultModalId = "code-gn-modal";
    var _defaultCodeGenerationExecuteBtnId = "code-generation-submit";
    var _pagesContainerId = 'pages-container';
    var _pageTabsContainerId = 'page-tabs';
    var _counter = 0;
    var _appendArea = "body";

    //set private properties
    var initiate = function (options) {
        _modalHtml = options.ModalHTML;
        _onModalOpenJS = options.OnModalOpenJS;
        _onExecuteCGJS = options.OnExecuteCGJS;
        _onAfterExecuteCGJS = options.OnAfterExecuteCGJS;
        _modalName = options.ModalName;
    };

    //appends modal to body
    var setModal = function () {
        if (_modalName == "") {
            _modalName = "[Untitled]";
        }
        var modal = '<div id="' + _defaultModalId + '" class="modal fade">\
  <div class="modal-dialog">\
    <div class="modal-content">\
      <div class="modal-header">\
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>\
        <h4 class="modal-title"><span class="glyphicon glyphicon-fire"></span> '+ _modalName + '</h4>\
      </div>\
      <div id="code-gn-body" class="modal-body">\
        \
      </div>\
      <div class="modal-footer">\
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>\
        <button type="button" class="btn btn-primary" id="'+ _defaultCodeGenerationExecuteBtnId + '">Generate Code</button>\
      </div>\
    </div>\
  </div>\
</div>';

        $('body').append(modal);
        $("#" + _defaultModalId + " .modal-body").html(_modalHtml);
    };

    //Appends pages container to body (used so i can append pages to it)
    var setPagesContainer = function () {
        var tabs = '<ul id="' + _pageTabsContainerId + '" class="nav nav-tabs"></ul>';
        var pagesContainer = '<div class="pages-container tab-content" id="' + _pagesContainerId + '"></div>';
        var cgComponentsContainer = '<div id="cg-components"></div>';

        $(_appendArea).append(cgComponentsContainer);
        $('#cg-components').append(tabs).append(pagesContainer);
    };

    //Responsible for setting the modal, the pages container
    //binding the onshow event, on click event to the code generation button inside the modal
    var start = function (cb) {
        setModal(); //append modal to boy
        setPagesContainer(); //append pages container to modal

        $('#' + _defaultModalId).unbind("show.bs.modal").on('show.bs.modal', function (e) {
            eval(_onModalOpenJS);
        });
        $("#" + _defaultModalId).modal('show');

        $("#" + _defaultCodeGenerationExecuteBtnId).unbind("click").click(function () {
            eval(_onExecuteCGJS);
            $('#' + _defaultModalId).modal("hide");
            showPages();
        });
    };

    //Inserts new page into the pages container. Must have the pages container already appended to the body
    //see SetPagesContainer func
    var newPage = function (pageName, pageCode, aceCodeType) {
        // create new page container for pages container
        var pageId = "pg" + _counter;
        var pageContainer = "<div id='" + pageId + "' data-page-name='" + pageName + "' data-code-type='" + aceCodeType + "' class='cg_pages tab-pane fade' ></div>";
        var newTab = '<li class=""><a data-toggle="tab" href="#' + pageId + '">' + pageName + '</a></li>';

        $('#' + _pageTabsContainerId).append(newTab);
        $('#' + _pagesContainerId).append(pageContainer);
        $('#' + pageId).data('the-code', pageCode);
        _counter++;
    };

    //workaround for HTML pages
    var processCode = function (aceEditor, html) {
        var $input = $("<textarea>");
        $input.val(html);
        var htmlCode = $input.val();

        aceEditor.setValue(htmlCode);
    };

    //displays pages container, sets the code editors
    var showPages = function () {
        $('#' + _pagesContainerId).find('.cg_pages').each(function (index, p) {
            $page = $(this);

            var pageName = $page.data('page-name');
            var codeType = $page.data('code-type');
            var theCode = $page.data('the-code');
            var pageId = $page.attr('id');
            var aceEditor = ace.edit(pageId);
            processCode(aceEditor, theCode);

            aceEditor.setTheme("ace/theme/chrome");
            aceEditor.getSession().setMode("ace/mode/" + codeType);
            aceEditor.setOptions(aceEditorOptions());
        });

        var $firstTab = $('#' + _pageTabsContainerId).find('li:first-child');
        $firstTab.children('a').click();

        //cb function after all code pages and tabs have been generated
        if (_onAfterExecuteCGJS != null)
            _onAfterExecuteCGJS();
    };

    var aceEditorOptions = function () {
        var options = {};
        options.showPrintMargin = false;

        return options;
    };

    var resetPages = function () {
        $('#' + _pageTabsContainerId).remove();
        $('#' + _pagesContainerId).remove();
    };
    var resetModal = function () {
        $('#' + _defaultModalId).remove();
    };
    var resetEverthing = function () {
        resetPages();
        resetModal();
    };
    var setAreaForInsertingPageContainer = function (area) {
        _appendArea = area;
    };

    return {
        initiate: initiate,
        start: start,
        newPage: newPage,
        resetEverything: resetEverthing,
        setAreaForInsertingPageContainer: setAreaForInsertingPageContainer
    };
}();