<script lang="ts">
	import Header from '$lib/components/Header.svelte';
</script>

<div class="flex flex-col items-center w-5/6 justify-center self-center space-y-10">
	<Header />

	<div class="w-full">
		<slot />
	</div>
</div>
