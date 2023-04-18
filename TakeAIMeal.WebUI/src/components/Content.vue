<template>
    <div class="page-container">
        <!-- Temporary code should be move into dashboard/home component-->
        <div class="content-container">
            <div class="content-header">
                <h1>Take a<sup>i</sup> meal</h1>
            </div>
            <div class="hint-container">
                <i class="icon-hint"></i>
                <div class="hint-slider">

                </div>
            </div>
            <div class="action-container">
                <div class="left-part">
                    <div>
                        <button class="btn btn-primary">Szybki przepis</button>
                    </div>
                    <ul>
                        <li>Wygeneruj danie na podstawie prostych wskazówek</li>
                    </ul>
                </div>
                <div class="right-part">
                    <div>
                        <button class="btn btn-secondary">Zarejestruj się</button>
                    </div>
                    
                    <ul>
                        <li>Personaluzuj dane dietetyczne</li>
                        <li>Wskaż produkty, których nie lubisz</li>
                        <li>Zapisuj przepisy w książce kucharskiej</li>
                    </ul>
                    <div class="sign-in-container">
                        <span>Masz konto?</span>
                        <button class="btn btn-primary">Zaloguj się</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'ContentComponent'
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
  