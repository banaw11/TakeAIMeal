<template>
    <div class="page-header">
        <nav class="navbar navbar-expand-lg navbar-light bg-white">
            <a class="navbar-brand" href="#">Take ai meal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                    <a class="nav-item nav-link active" href="#">Strona główna <span class="sr-only">(current)</span></a>
                    <a class="nav-item nav-link" href="#">O aplikacji</a>
                    <a class="nav-item nav-link" href="#">Zaloguj się</a>
                    <a class="nav-item nav-link disabled" href="#">Disabled</a>
                </div>
            </div>
            <div class="language-container">
                <i class="icon-flag pl"></i>
                <i class="icon-flag en"></i>
            </div>
        </nav>

        <!-- TODO: nie działa "hamburger" na widoku mobilnym -->
    </div>
</template>

<script>
    export default {
        name: 'NavComponent'
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
  