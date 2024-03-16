<script>
	import ThemeToggle from '$lib/components/ThemeToggle.svelte';
	import Logo from '$lib/components/icons/Logo.svelte';
</script>

<div class="flex flex-col min-h-screen">
	<div class="flex-auto flex flex-col">
		<slot />
	</div>
	<footer class="flex-initial flex flex-col shadow-lg">
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
</div>
