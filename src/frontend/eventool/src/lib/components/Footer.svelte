<script>
	import ThemeToggle from "./ThemeToggle.svelte";


	import Logo from "./icons/Logo.svelte";

</script>
<footer class="flex-initial flex flex-col mt-auto">
    <div class="px-10 py-5">
        <div class="flex flex-row space-y-3 justify-between items-center content-center">
            <div class="flex flex-col space-y-2">
                <Logo size={112} />
                <p class="text-sm text-base-content/50">© 2024, eventool</p>
            </div>
            <ThemeToggle />
        </div>
    </div>
</footer>