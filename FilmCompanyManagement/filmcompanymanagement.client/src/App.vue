<template>
    <h1>FCM</h1>

    <main>
        <button @click="testConnection">Test Connection</button>
        <div v-if="connectionResult!==null">
            <p>{{connectionResult}}</p>
        </div>
    </main>
</template>

<script>
    export default {
        data() {
            return {
                connectionResult: null
            }
        },
        methods: {
            async testConnection() {
                alert("shuo de dao li");
                fetch("https://localhost:7142/api/Test")
                    .then(response => {
                        if (!response.ok) throw new Error("Test Failed");
                        return response.text();
                    })
                    .then(result => {
                        this.connectionResult = result;
                    })
                    .catch(error => {
                        console.error("Error testing database connection:", error);
                        this.connectionResult = "Failed to connect to database. Please check the console for details.";
                    });
            }
        }
    }
</script>