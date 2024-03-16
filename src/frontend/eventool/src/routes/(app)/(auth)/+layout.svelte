<script>
	import Logo from '$lib/components/icons/Logo.svelte';
</script>

<div class="flex-auto flex flex-col justify-center items-center">
	<div class="flex flex-col justify-center items-center space-y-5">
		<a href="/" class="text-primary">
			<Logo size={300} />
		</a>
		<div class="max-w-lg">
			<slot />
		</div>
	</div>
</div>
