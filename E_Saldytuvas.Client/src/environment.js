const environmentConfig = {
    development: {
        backEndUrl: "http://localhost:57344",
        frontEndUrl: "http://localhost:3000"
    }
};

export default environmentConfig;

export function getBackEndUrl() {
    return environmentConfig[process.env.NODE_ENV].backEndUrl;
}

export function getFrontEndUrl() {
    return environmentConfig[process.env.NODE_ENV].frontEndUrl;
}