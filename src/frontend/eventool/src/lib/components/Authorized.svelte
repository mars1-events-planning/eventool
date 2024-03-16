
<script lang="ts">
	import { goto } from '$app/navigation';
	import Preloader from '$lib/components/icons/Preloader.svelte';
	import { graphql } from '$houdini';

	const authorizedStore = graphql(`
		query IsAuthorized {
			authorized
		}
	`);

	let authorized = $state(false);

	let p = async () => {
		let data = await authorizedStore.fetch();

		if (data.data?.authorized === false) {
			await goto('/error');
			return;
		}
		authorized = true
	};
</script>

{#await p()}
	<Preloader />
{:then}
	{#if authorized}
		<slot />
	{/if}
{/await}
