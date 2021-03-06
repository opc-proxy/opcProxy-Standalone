import sys
sys.path.insert(0, "..")
import time


from opcua import ua, Server


if __name__ == "__main__":

    # setup our server
    server = Server()
    server.set_endpoint("opc.tcp://0.0.0.0:4840/freeopcua/server/")

    # setup our own namespace, not really necessary but should as spec
    uri = "http://examples.freeopcua.github.io"
    idx = server.register_namespace(uri)

    # get Objects node, this is where we should put our nodes
    objects = server.get_objects_node()

    # populating our address space
    myobj = objects.add_object(idx, "MyObject")
#    myvar = myobj.add_variable(idx, "PV01", False)
#    myvar.set_writable()    # Set MyVariable to be writable by clients
#    myvar = myobj.add_variable(idx, "SPV01O", 0)
#    myvar.set_writable()    # Set MyVariable to be writable by clients
    myvar = myobj.add_variable(idx, "Req_PV01", False )
    myvar.set_writable()    # Set MyVariable to be writable by clients
    myvar = myobj.add_variable(idx, "", 6.7)
    myvar.set_writable()    # Set MyVariable to be writable by clients

    list_var = []
    for x in range(0,1000) :
        myobj.add_variable(idx,"PV0" + str(x), False).set_writable()
        myobj.add_variable(idx,"SPV0" + str(x) +"O", False).set_writable()
        gigio = myobj.add_variable(idx,"MyVar" + str(x) , 1.0) 
        gigio.set_writable()
        list_var.append(gigio) 
        
    myvar = myobj.add_variable(idx, "MyVar", 1.0 )

    # starting!
    server.start()
    
    try:
        count = 1
        myvar.set_value(count)

        while True:
            time.sleep(1)
            count = myvar.get_value()
            count += 0.1
            myvar.set_value(count)
            #for v in list_var:
            #    v.set_value(count)

    finally:
        #close connection, remove subcsriptions, etc
        server.stop()
