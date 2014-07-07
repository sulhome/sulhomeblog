app.kanbanBoardApp.service('boardService', function ($, $http, $q, $rootScope) {

    var setupHttpCall = function (callingMethod, methodName, params) {
        var httpCallParam = {
            method: callingMethod,
            url: methodName,
        };

        if (params != null) {
            httpCallParam['params'] = params;
        }

        var deferred = $q.defer();
        $http(httpCallParam)
        .success(function (data, status, headers, config) {
            deferred.resolve(data);
        })
        .error(function (data, status) {
            deferred.reject(data);
        });
        return deferred.promise;
    }

    var getColumns = function () {
        return setupHttpCall('GET', 'GetColumns', null);
    }

    var canMoveTask = function (sourceColIdVal, targetColIdVal) {
        return setupHttpCall('GET', 'CanMove', { sourceColId: sourceColIdVal, targetColId: targetColIdVal });
    }

    var moveTask = function (taskIdVal, targetColIdVal) {
        return setupHttpCall('POST', 'MoveTask', { taskId: taskIdVal, targetColId: targetColIdVal });     
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