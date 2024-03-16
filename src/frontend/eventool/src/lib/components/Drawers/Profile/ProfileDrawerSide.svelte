<script lang="ts">
	import AvatarPlaceholder from '$lib/components/AvatarPlaceholder.svelte';
	import Authorized from '../../Authorized.svelte';

	let { ...props } = $props<{
		initialFullname: string;
		initialUsername: string;
	}>()
</script>

<Authorized>
	<div class="menu w-1/2 min-h-full bg-base-100 text-base-content p-10">
		<div class="flex flex-col space-y-5">
			<h1 class="font-bold text-xl">Ваш профиль</h1>
			<div class="flex flex-row justify-start space-x-4">
				<div class="flex flex-col">
					<AvatarPlaceholder avatarClasses="rounded-lg p-2 w-36 h-36">
						<span class="text-4xl">{props.initialFullname.slice(0, 2).toUpperCase()}</span>
					</AvatarPlaceholder>
				</div>
			</div>
		</div>
	</div>
</Authorized>
