<#
.SYNOPSIS
This script generates basic code for cards if the code file doesn't exist yet.

.DESCRIPTION
This script generates basic code for cards if the code file doesn't exist yet. It takes
a json file containing details about the card and uses it to generate
.cs files based on template files for that card type.

NOTE: Dependency on Powershell 4.0+
#>

function GenerateCodeFile($card)
{
    $cardDir = "..\Cards"
    $minionsDir = Join-Path $cardDir "Minions"
    $spellsDir = Join-Path $cardDir "Spells"
    $weaponsDir = Join-Path $cardDir "Weapons"

    $minionTemplate = "TemplateMinion.cs.txt"
    $spellTemplate = "TemplateSpell.cs.txt"
    $weaponTemplate = "TemplateWeapon.cs.txt"

    $className = [System.Text.RegularExpressions.Regex]::Replace($card.name, "[\W]", "")

    $fileName = $className + '.cs'

    $attack = @{$true=0;$false=$card.attack}[$card.attack -eq $null]
    $health = @{$true=0;$false=$card.health}[$card.health -eq $null]
    $mana = @{$true=0;$false=$card.cost}[$card.cost -eq $null]

    if ($card.type -eq 'minion')
    {
        $filePath = Join-Path $minionsDir $fileName
        if (Test-Path $filePath)
        {
            Write-Warning "$filePath already exists, skipping file generation."
            continue
        }

        # File hasn't been created yet, so let's generate it
        (Get-Content $minionTemplate) | Foreach-Object {
            $_.Replace("_CLASS_NAME_", $className). `
            Replace("_NAME_", $card.name). `
            Replace("_MANA_COST_", $mana). `
            Replace("_ATTACK_POWER_", $attack). `
            Replace("_HEALTH_", $health)
        } | Set-Content $filePath
    }
    elseif ($card.type -eq 'weapon')
    {
        $filePath = Join-Path $weaponsDir $fileName
        if (Test-Path $filePath)
        {
            Write-Warning "$filePath already exists, skipping file generation."
            continue
        }

        (Get-Content $weaponTemplate) | Foreach-Object {
            $_.Replace("_CLASS_NAME_", $className). `
            Replace("_NAME_", $card.name). `
            Replace("_MANA_COST_", $mana). `
            Replace("_ATTACK_POWER_", $attack). `
            Replace("_DURABILITY_", $health)
        } | Set-Content $filePath
    }
	elseif ($card.type -eq 'spell')
	{
		$filePath= Join-Path $spellsDir $fileName
		if (Test-Path $filePath)
		{
			Write-Warning "$filePath already exists, skipping file generation."
			continue
		}

		$minSpellPower = 0
		$maxSpellPower = 0

		if ($card.text -match '\$([0-9]+).+\$([0-9]+)')
		{
			$minSpellPower = $matches[1]
			$maxSpellPower = $matches[1]

			if ($matches.Count -eq 3)
			{	
				$maxSpellPower = $matches[2]
			}
		}

        (Get-Content $spellTemplate) | Foreach-Object {
            $_.Replace("_CLASS_NAME_", $className). `
            Replace("_NAME_", $card.name). `
            Replace("_MANA_COST_", $mana). `
            Replace("_CARD_TEXT_", $card.text). `
            Replace("_MIN_SPELL_POWER_", $minSpellPower). `
            Replace("_MAX_SPELL_POWER_", $maxSpellPower)
        } | Set-Content $filePath
	}
    else
    {
        Write-Warning "Don't know how to handle $($card.type) yet."
    }
}

$cards = (Get-Content all-cards.json) -join "`n" | ConvertFrom-Json

foreach ($card in $cards.Basic)
{
    GenerateCodeFile($card)
}
foreach ($card in $cards.'Curse of Naxxramas')
{
    GenerateCodeFile($card)
}
foreach ($card in $cards.Expert)
{
    GenerateCodeFile($card)
}
foreach ($card in $cards.Missions)
{
    GenerateCodeFile($card)
}
foreach ($card in $cards.Promotion)
{
    GenerateCodeFile($card)
}
foreach ($card in $cards.Reward)
{
    GenerateCodeFile($card)
}