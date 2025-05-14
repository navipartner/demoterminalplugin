let _mockContext;
const hwc = window._np_hardware_connector;

function log(text, error) {
    const entry = document.createElement("div");
    entry.classList.add("entry");
    if (error)
        entry.classList.add("error");
    entry.innerText = typeof text === "string" ? text : JSON.stringify(text);
    if (text && text.success === false)
        entry.classList.add("warning");

    document.getElementsByClassName("log")[0].appendChild(entry);
}

async function callHwc(delegate) {
    try {
        const result = await delegate();
        log(result);
    }
    catch (e) {
        log(e && e.message || e || "Unknown error", true);
    }
}

async function MockResetContext() {
    if (_mockContext) {
        hwc.unregisterResponseHandler(_mockContext);
    }

    _mockContext = hwc.registerResponseHandler(msg => {
        let logLine = _mockContext + " " + JSON.stringify(msg);
        log(logLine);
        console.log(logLine);
    });
}

async function testEcho() {
    return await hwc.sendRequestAndWaitForResponseAsync(
        "Echo",
        {
            hello: "World!",
            time: Date.now()
        }
    );
}

async function DemoTerminal() {
    return await hwc.sendRequestAsync(
        "DemoTerminal",
        {
            TransactionId: "01de18eb-c32b-4585-a78d-addcc3cf2dce",
            Type: "Transaction",
            Amount: 100.00,
            TerminalIP: "192.168.1.16"
        },
        _mockContext
    )
}


   