sulhome.kanbanBoardApp.service('boardService', function ($http, $q, $rootScope) {
    var proxy = null;

    var getColumns = function () {
        return $http.get("/api/BoardWebApi").then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data.Message);
        });
    };

    var canMoveTask = function (sourceColIdVal, targetColIdVal) {
        return $http.get("/api/BoardWebApi/CanMove", { params: { sourceColId: sourceColIdVal, targetColId: targetColIdVal } })
            .then(function (response) {
                return response.data.canMove;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var moveTask = function (taskIdVal, targetColIdVal) {
        return $http.post("/api/BoardWebApi/MoveTask", { taskId: taskIdVal, targetColId: targetColIdVal })
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    
    var initialize = function () {

        connection = jQuery.hubConnection();
        this.proxy = connection.createHubProxy('KanbanBoard');

        // Listen to the 'BoardUpdated' event that will be pushed from SignalR server
        this.proxy.on('BoardUpdated', function () {
            $rootScope.$emit("refreshBoard");
        });

        // Connecting to SignalR server        
        return connection.start()
        .then(function (connectionObj) {
            return connectionObj;
        }, function (error) {
            return error.message;
        });
    };

    // Call 'NotifyBoardUpdated' on SignalR server
    var sendRequest = function () {        
        this.proxy.invoke('NotifyBoardUpdated');
    };

    return {
        initialize: initialize,
        sendRequest: sendRequest,
        getColumns: getColumns,
        canMoveTask: canMoveTask,
        moveTask: moveTask
    };
});