/// <references types="houdini-svelte">

/** @type {import('houdini').ConfigFile} */
const config = {
    "watchSchema": {
        "url": "http://localhost:12000/graphql",
        "headers": {
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjIyMTUzMzM0NzIsImlzcyI6ImV2ZW50b29sIiwiYXVkIjoiZXZlbnRvb2wifQ.16_DKImkI5yXxxY-9vEB3MFYBcSAP_aO4rc2fd-TBqU"
        }
    },
    "plugins": {
        "houdini-svelte": {
            quietQueryErrors: true
        }
    },
    scalars: {
        "UUID": {
            type: "string"
        }
    }
}

export default config
