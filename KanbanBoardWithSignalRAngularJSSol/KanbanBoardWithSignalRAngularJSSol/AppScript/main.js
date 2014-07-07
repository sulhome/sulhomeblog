// application global namespace
app = {};
app.kanbanBoardApp = angular.module('kanbanBoardApp', ['ngDragDrop', 'ui.bootstrap']);
app.kanbanBoardApp.value('$', $);
