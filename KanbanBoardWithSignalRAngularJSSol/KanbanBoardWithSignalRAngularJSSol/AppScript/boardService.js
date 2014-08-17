app.kanbanBoardApp.service('boardService', function ($, $http, $q, $rootScope) {

    var getColumns = function () {        
        return $http.get("/api/BoardWebApi").then(function (response) {
            return response.data;
        });        
    }

    var canMoveTask = function (sourceColIdVal, targetColIdVal) {
        return $http.get("/api/BoardWebApi/CanMove", { params: { sourceColId: sourceColIdVal, targetColId: targetColIdVal } })
            .then(function (response) {
                return response.data.canMove;
        });        
    }

    var moveTask = function (taskIdVal, targetColIdVal) {
        return $http.post("/api/BoardWebApi/MoveTask", { taskId: taskIdVal, targetColId: targetColIdVal } )
            .then(function (response) {
                return response.status == 200;
            });          
    };

    var proxy = null;

    var initialize = function () {
        //Getting the connection object
        connection = $.hubConnection();

        //Creating proxy
        this.proxy = connection.createHubProxy('KanbanBoard');

        //Publishing an event when server pushes a greeting message
        this.proxy.on('BoardUpdated', function () {
            $rootScope.$emit("refreshBoard");
        });

        var deferred = $q.defer();
        
        //Starting connection
        connection.start().done(function () 
        { 
            deferred.resolve(connection);
        })
        .fail(function () { deferred.reject(); });
                
        return deferred.promise;
    };

    var sendRequest = function () {
        //Invoking greetAll method defined in hub
        this.proxy.invoke('BoardUpdated');
    };

    return {
        initialize: initialize,
        sendRequest: sendRequest,
        getColumns: getColumns,
        canMoveTask: canMoveTask,
        moveTask: moveTask
    };

});