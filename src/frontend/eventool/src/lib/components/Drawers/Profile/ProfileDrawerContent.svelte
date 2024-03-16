<script lang="ts">
	import AvatarPlaceholder from '$lib/components/AvatarPlaceholder.svelte';
	import Authorized from '../../Authorized.svelte';

	let {name} = $props<{name: string}>()
</script>

<Authorized>
	<label for="profile-drawer" class="btn btn-ghost drawer-button btn-circle">
		
	</label>
</Authorized>
